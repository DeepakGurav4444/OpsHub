using AutoMapper;
using OpsHubBAL.SubPlanModMap;
using OpsHubCommonUtility.Logger;
using OpsHubCommonUtility.Response;
using OpsHubDAL.DataModel;
using OpsHubDAL.SubPlan;
using OpsHubDAL.TenantRepo;
using OpsHubDAL.TenModRepo;
using OpsHubDAL.TenSubMapRepo;
using OpsHubDTOModel.SubPlan;
using OpsHubDTOModel.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsHubBAL.Tenant
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ISubScriptionPlanRepository _subScriptionPlanRepository;
        private readonly ISubPlanModMapRepository _subPlanModMapRepository;
        private readonly ITenantModuleRepository _tenantModuleRepository;
        private readonly ITenantSubMapRepository _tenantSubMapRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public TenantService(ITenantRepository tenantRepository,ITenantModuleRepository tenantModuleRepository, ISubScriptionPlanRepository subScriptionPlanRepository, ISubPlanModMapRepository subPlanModMapRepository, ITenantSubMapRepository tenantSubMapRepository, ILoggerManager loggerManager,IMapper mapper) 
        {
            _tenantRepository = tenantRepository;
            _tenantModuleRepository = tenantModuleRepository;
            _subScriptionPlanRepository = subScriptionPlanRepository;
            _tenantSubMapRepository = tenantSubMapRepository;
            _subPlanModMapRepository = subPlanModMapRepository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<ResultWithDataDTO<int>> RegisterTenant(AddTenantsRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry TenantService => AddTenants");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            var tenInsResult = await _tenantRepository.AddTenant(_mapper.Map<TenTenants>(request_DTO));
            if (tenInsResult != null)
            {    
                resultWithDataBO.Data = 1;
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Tenant added successfully.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            else
            {
                resultWithDataBO.IsBusinessError = true;
                resultWithDataBO.BusinessErrorMessage = $"Failed to add tenant, Kindly contact administrator.";
                _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
            }
            _loggerManager.LogInfo("Exit TenantService => AddTenants");
            return resultWithDataBO;
        }

        public async Task<ResultWithDataDTO<int>> SubscribeToPlan(SubscribePlanRequest_DTO request_DTO)
        {
            _loggerManager.LogInfo("Entry TenantService => SubscribeToPlan");
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            List<int> mappedModulesList = new List<int>();
            var alredayMappedPlans = await _tenantSubMapRepository.GetTenSubMap(request_DTO.TenantId);
            if (alredayMappedPlans.Any()) 
            {
                var alredayMappedModules = await _subPlanModMapRepository.GetSubPlanModListBySubId(alredayMappedPlans.Select(x=>x.SubPlanId).ToList());
                mappedModulesList = alredayMappedModules.Select(x=>x.ModuleId).ToList();
            }
            var subscriptionPlan = await _subScriptionPlanRepository.GetSubPlanById(request_DTO.SubPlanId);
            if (subscriptionPlan != null) 
            {
                var modulesMapForPlan = await _subPlanModMapRepository.GetSubPlanModMapBySubId(request_DTO.SubPlanId);
                if (!mappedModulesList.Intersect(modulesMapForPlan.Select(x => x.ModuleId).ToList()).Any())
                {
                    var addMapTenPlanResult = await _tenantSubMapRepository.AddTenSubPlan(new TenSubMap() 
                    {
                        SubPlanId = request_DTO.SubPlanId,
                        TenantId = request_DTO.TenantId
                    });
                    if (addMapTenPlanResult != null)
                    {
                        var addTenModeulesResult = await _tenantModuleRepository.AddTenantModules(modulesMapForPlan.Select(mp=>new TenModules() 
                        {
                            ExpiresOn = DateTime.UtcNow.AddMonths(subscriptionPlan.DurationInMonth),
                            ModulesId = mp.ModuleId,
                            PurchaseDate =  DateOnly.FromDateTime(DateTime.UtcNow),
                            TenantId = request_DTO.TenantId
                        }).ToList());
                        if (addTenModeulesResult != 0)
                        {
                            resultWithDataBO.Data = 1;
                            resultWithDataBO.IsSuccessful = true;
                            resultWithDataBO.Message = $"Plan subscribed successfully.";
                        }
                        else 
                        {
                            resultWithDataBO.IsBusinessError = true;
                            resultWithDataBO.BusinessErrorMessage = $"Failed to add tenant modules, Kindly contact administrator.";
                            _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                        }
                    }
                    else 
                    {
                        resultWithDataBO.IsBusinessError = true;
                        resultWithDataBO.BusinessErrorMessage = $"Failed to map tenant plan, Kindly contact administrator.";
                        _loggerManager.LogError(resultWithDataBO.BusinessErrorMessage);
                    }
                }
                else
                {
                    resultWithDataBO.IsSuccessful = true;
                    resultWithDataBO.Message = $"Modules alreday present.";
                    _loggerManager.LogInfo(resultWithDataBO.Message);
                }
            }
            else 
            {
                resultWithDataBO.IsSuccessful = true;
                resultWithDataBO.Message = $"Subscription plan not found.";
                _loggerManager.LogInfo(resultWithDataBO.Message);
            }
            _loggerManager.LogInfo("Exit TenantService => SubscribeToPlan");
            return resultWithDataBO;
        }
    }
}
