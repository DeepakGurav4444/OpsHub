# Role-Based Multi-Tenant Authorization System (Web API)

## 🔍 Overview

This project is a **.NET 8 Web API** built using a three-layered architecture, designed to support a **multi-tenant SaaS platform** with **role-based access control**, **dynamic policy authorization**, and **external client notification support**.

It enables different roles like System Admin, Tenant, Super Admin, and Operational Users to interact securely and independently based on scoped responsibilities and policies.

---

## 🏗️ Architecture

- **Three-Layered Design**
  - Controllers → Services → Repositories
- **Custom Token-Based Login Flow**
- **JWT Auth Token** for subsequent requests
- **Policy-Based Authorization**
- **PermissionHandler** for dynamic role/module/workflow evaluation
- **Model Validation** using Action Filters
- **Global Exception Handling**
- **Transaction Management**
- **Client Registration and Notification Support**

---

## 🔐 Authentication & Authorization Flow

- All users (System Admin, Tenant, Super Admin, Operational User) **login using a Custom Token**
- After login, users are issued an **Auth Token (JWT)** for all subsequent API calls
- Access is controlled via **role-based and module-specific policies**
- Fine-grained rules applied using:
  - `PermissionHandler`
  - Scoped Module Access
  - Workflow Step Permissions

---

## 👥 Role Responsibilities

### 🛡️ System Admin
- **Login**: Custom Token
- **After Auth**: Manages tenants and token permissions
- **Actions**:
  - Add new tenants
  - Define tenant module access and initial configurations

### 🏢 Tenant
- **Login**: Custom Token
- **After Auth**: Subscribes to plans and manages module Super Admins
- **Actions**:
  - Add Super Admins to modules
  - Define role-based workflows
  - Subscribe to available plans

### ⚙️ Super Admin (Per Module)
- **Login**: Custom Token
- **After Auth**: Manages operational users and roles within modules
- **Actions**:
  - Create operational users
  - Assign them roles and permissions
  - Define and manage workflows

### 👷 Operational User
- **Login**: Custom Token
- **After Auth**: Operates within their assigned roles/modules
- **Actions**:
  - Perform workflow-specific or module-limited actions
  - Register and manage external clients if permitted

---

## 👥 Client Management and Notification

- Logged-in users can **register external clients** (e.g., visitors, contractors, drivers) under their assigned module.
- Clients can also **self-register** under a specific module user, depending on the module's configuration.
- Users can **send notifications** to their assigned clients (e.g., visitor approval, gate pass issued).
- All client interactions and notifications are **secured via policy-based authorization**, ensuring that only permitted users can register or communicate with clients.

---

## ✅ Key Features

- 🔐 Custom Token Login for all core users
- 🪪 Auth Token for API access (JWT-based)
- 🧠 Dynamic Policy-Based Authorization
- 🏢 Tenant-Based Module & Workflow Segregation
- 👥 External Client Registration & Secure Notification
- 🧩 Configurable PermissionHandler & Action Filters
- 🚨 Global Exception Handling
- 🔄 Transaction-Safe Data Processing

---

## 📂 Folder Structure (Typical)

