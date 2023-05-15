resource "azurerm_service_plan" "tempus-asp" {
  name                = "tempus-asp"
  location            = var.location
  resource_group_name = var.rg_name
  os_type             = "Linux"

  sku_name = var.app_plan_tier

  tags = {
    environment = var.environment
  }
}

resource "azurerm_linux_web_app" "tempus-app" {
  name                = "${var.app_name}-app-dev"
  resource_group_name = var.rg_name
  location            = azurerm_service_plan.tempus-asp.location
  service_plan_id     = azurerm_service_plan.tempus-asp.id

  app_settings = {
    "KeyVaultName"                        = var.kv_name
    APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.tempus_app_insights.connection_string
    APPINSIGHTS_INSTRUMENTATIONKEY        = azurerm_application_insights.tempus_app_insights.instrumentation_key
  }

  site_config {}

  identity {
    type = "SystemAssigned"
  }

  tags = {
    environment = var.environment
  }
}


resource "azurerm_log_analytics_workspace" "tempus_workspace" {
  name                = "tempus-log-dev"
  location            = var.location
  resource_group_name = var.rg_name
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = {
    environment = var.environment
  }
}

resource "azurerm_application_insights" "tempus_app_insights" {
  name                 = "tempus-appi-dev"
  location             = var.location
  resource_group_name  = var.rg_name
  workspace_id         = azurerm_log_analytics_workspace.tempus_workspace.id
  daily_data_cap_in_gb = 0.2
  application_type     = "web"

  tags = {
    environment = var.environment
  }
}
