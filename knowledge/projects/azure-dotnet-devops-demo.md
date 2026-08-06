---
title: Azure .NET DevOps Platform

organization: School Project

role: Backend Developer

period:
  from: 2026-07
  to: 2026-07

status: completed

technologies:
  - aspnet-core
  - azure
  - container-registry
  - csharp
  - dotnet
  - docker
  - github-actions
  - linux
  - terraform

concepts:
  - api-design
  - cloud-infrastructure
  - infrastructure-as-code
  - ci-cd
  - containerization
  - deployment-automation
  - devops
  - security
  - managed-identity

links:

  github:
  live: Not available

---

# Overview

A cloud deployment project demonstrating how a modern ASP.NET Core Web API can be provisioned, containerized, deployed, and updated using Infrastructure as Code and CI/CD automation.

The project combines Terraform, Azure, Docker, and GitHub Actions into a complete deployment pipeline where infrastructure provisioning, container deployment, and cloud authentication workflows are automated.

The primary goal was to gain practical experience with modern DevOps practices while reducing manual deployment steps and improving infrastructure reproducibility.

---

# Context

Modern backend development requires understanding not only application code, but also infrastructure, deployment automation, security, and operational workflows.

Many developers are comfortable building APIs locally but have limited experience provisioning cloud infrastructure or creating automated deployment pipelines.

The project was created to explore how modern DevOps practices can be integrated into an ASP.NET Core application while following repeatable and secure deployment principles.

Important constraints:

- Infrastructure should be reproducible.
- Deployments should require minimal manual work.
- Secrets should not be stored in source control.
- CI/CD authentication should follow Azure security best practices.
- The deployment pipeline should support future application updates.
- Infrastructure configuration should remain version controlled.

---

# Task

My responsibility was designing and implementing the complete cloud deployment pipeline.

This included:

- Designing the Azure cloud architecture.
- Building the ASP.NET Core Web API.
- Containerizing the application using Docker.
- Provisioning Azure infrastructure using Terraform.
- Creating automated GitHub Actions workflows.
- Configuring secure CI/CD authentication using Azure OpenID Connect.
- Configuring User Assigned Managed Identity for Azure Container Registry access.
- Automating application deployment to Azure Virtual Machines.

The objective was to create a repeatable deployment process requiring minimal manual intervention.

---

# Challenge

## Challenge: Secure Automated Cloud Deployment

### Problem

Deploying applications to cloud infrastructure requires managing infrastructure provisioning, container distribution, authentication, and deployment sequencing.

Handling these processes manually increases operational complexity and introduces opportunities for configuration errors.

The deployment process required:

- Reproducible cloud infrastructure.
- Secure authentication without storing long-lived credentials.
- Automated container image building and distribution.
- Reliable application updates on the target environment.

---

### Solution

The deployment process was automated using:

- Terraform for infrastructure provisioning.
- Docker for application packaging.
- GitHub Actions for CI/CD automation.
- Azure Container Registry for container image storage.
- Azure OpenID Connect for GitHub Actions authentication.
- User Assigned Managed Identity for Azure Container Registry access.

GitHub Actions authenticates with Azure through OpenID Connect, eliminating the need to store Azure client secrets inside the repository.

The deployment pipeline:

1. Authenticates to Azure using OpenID Connect.
2. Builds the Docker image.
3. Pushes the image to Azure Container Registry.
4. Connects to the Azure Virtual Machine through SSH.
5. Uses the VM Managed Identity to authenticate against Azure Container Registry.
6. Pulls the latest container image using Docker Compose.
7. Restarts the application container.

---

### Result

The project demonstrates a repeatable deployment workflow where infrastructure and application deployments can be recreated consistently while following modern Azure authentication practices.

---

# Action

## Architecture

### Backend

The backend consists of an ASP.NET Core Web API packaged as a Docker container.

Responsibilities include:

- HTTP API endpoints.
- Business logic.
- Dependency Injection.
- Configuration management.
- Containerized application execution.

---

### Database

No persistent database was required for this demonstration.

The architecture was intentionally designed so database services could be introduced later without requiring changes to the deployment pipeline.

---

### Infrastructure

Infrastructure is managed entirely through Terraform.

Provisioned Azure resources include:

- Resource Group.
- Virtual Network.
- Subnet.
- Network Security Group.
- Public IP.
- Linux Virtual Machine.
- Azure Container Registry.
- User Assigned Managed Identity.
- AcrPull role assignment.

Application deployment is handled through GitHub Actions.

The pipeline builds the Docker image, pushes it to Azure Container Registry, connects to the Azure Virtual Machine through SSH, retrieves the latest image using Docker Compose, and restarts the running container.

---

# Technical Decisions

## Decision: Infrastructure as Code with Terraform

### Context

Cloud resources should be reproducible, version controlled, and easy to recreate.

Manually creating infrastructure through cloud dashboards increases the risk of configuration drift.

---

### Chosen Solution

Terraform was used to define and provision all Azure infrastructure.

---

### Alternatives Considered

- Azure Portal.
- Azure CLI scripts.

---

### Trade-offs

Advantages:

- Repeatable deployments.
- Version controlled infrastructure.
- Easier maintenance.
- Easier onboarding.
- Reduced configuration drift.

Disadvantages:

- Additional learning curve.
- More initial configuration work.

---

## Decision: OpenID Connect Authentication for CI/CD

### Context

Deployment pipelines require secure authentication with Azure.

Storing long-lived credentials inside GitHub repositories creates unnecessary security risks.

---

### Chosen Solution

GitHub Actions authenticates with Azure using OpenID Connect.

This allows temporary authentication tokens to be issued during workflow execution without storing Azure client secrets.

---

### Alternatives Considered

- Azure Client Secret authentication.
- Publish Profiles.

---

### Trade-offs

Advantages:

- No long-lived Azure secrets.
- Improved security posture.
- Easier credential management.

Disadvantages:

- More initial Azure configuration.
- Requires understanding of identity federation.

---

## Decision: User Assigned Managed Identity for Container Access

### Context

The deployed application environment needed secure access to Azure Container Registry without storing registry credentials.

---

### Chosen Solution

A User Assigned Managed Identity was attached to the Azure Virtual Machine and granted the `AcrPull` role.

The VM uses this identity to authenticate when pulling Docker images from Azure Container Registry.

---

### Alternatives Considered

- Storing container registry credentials.
- Using static access tokens.

---

### Trade-offs

Advantages:

- No stored registry credentials.
- Azure-managed identity lifecycle.
- Better security model.

Disadvantages:

- Additional Azure identity configuration.
- Requires understanding of Azure role assignments.

---

# Implementation

Implemented:

- ASP.NET Core Web API.
- Docker containerization.
- Terraform-managed Azure infrastructure.
- Azure Virtual Machine deployment.
- Azure Container Registry integration.
- GitHub Actions CI/CD pipeline.
- Azure OpenID Connect authentication for workflows.
- User Assigned Managed Identity for ACR access.
- Automated Docker image deployment workflow.
- Docker Compose based container updates.

---

# Result

The completed solution demonstrates an end-to-end cloud deployment pipeline using modern .NET and Azure tooling.

The project shows practical experience with:

- Infrastructure as Code.
- Containerized applications.
- Azure cloud provisioning.
- CI/CD automation.
- Secure cloud authentication.
- Automated application deployment.
- Version-controlled infrastructure.

Deployments can be repeated with minimal manual work while infrastructure remains defined and managed through code.

---

# Lessons Learned

This project significantly improved my understanding of cloud deployment and DevOps workflows.

Key lessons include:

- Infrastructure should be treated as code.
- CI/CD pipelines become easier to maintain when infrastructure is automated.
- OpenID Connect provides a more secure authentication model than stored client secrets.
- Managed Identity reduces the need for manual credential management.
- Docker improves deployment consistency across environments.
- Automated deployment pipelines reduce operational errors.

---

# Interview Notes

## Possible Questions

### Why Terraform instead of creating Azure resources manually?

Terraform allows infrastructure to be version controlled, repeatable, and reproducible across environments.

---

### Why use Docker?

Docker ensures the application behaves consistently regardless of where it is deployed by packaging the application and its runtime dependencies together.

---

### Why Managed Identity?

Managed Identity removes the need for storing credentials and provides secure Azure resource access through Azure role-based permissions.

---

### Why OpenID Connect?

OIDC allows GitHub Actions to authenticate securely with Azure without storing long-lived Azure credentials inside the repository.

---

### How does the deployment pipeline work?

The pipeline authenticates with Azure using OIDC, builds the Docker image, pushes it to Azure Container Registry, connects to the Azure VM through SSH, pulls the updated image using Docker Compose, and restarts the container.

---

# Key Talking Points

- Automated complete cloud deployment pipeline.
- Infrastructure as Code using Terraform.
- Azure resource provisioning.
- Docker containerization.
- GitHub Actions CI/CD automation.
- Secure authentication using OpenID Connect.
- Azure Container Registry integration.
- User Assigned Managed Identity.
- Version-controlled infrastructure.

---

# Future Improvements

Possible future improvements:

- Deploy to Azure App Service or Azure Container Apps.
- Introduce Kubernetes using Azure Kubernetes Service (AKS).
- Add monitoring with Azure Monitor and Application Insights.
- Add automated integration testing before deployment.
- Introduce multi-environment deployments (development, staging, production).
- Add centralized logging and alerting.
- Add automated rollback strategies for failed deployments.

---