resource "azurerm_service_plan" "tempus_asp" {
  name                = "tempus-asp"
  location            = var.location
  resource_group_name = var.rg_name
  os_type             = "Linux"

  sku_name = var.app_plan_tier

  tags = {
    environment = var.environment
  }
}

# resource "azurerm_service_plan" "tempus_asp_UI" {
#   name                = "tempus-asp-UI"
#   location            = var.location
#   resource_group_name = var.rg_name
#   os_type             = "Windows"

#   sku_name = var.app_plan_tier

#   tags = {
#     environment = var.environment
#   }
# }

resource "azurerm_linux_web_app" "tempus_app" {
  name                = "tempus-monolith"
  resource_group_name = var.rg_name
  location            = azurerm_service_plan.tempus_asp.location
  service_plan_id     = azurerm_service_plan.tempus_asp.id

  app_settings = {
    "KeyVaultName"                        = var.kv_name
  }

  site_config {
    application_stack {
      dotnet_version = "7.0"
    }
  }

  identity {
    type = "SystemAssigned"
  }

  tags = {
    environment = var.environment
  }
}


# resource "azurerm_windows_web_app" "tempus_app_UI" {
#   name                = "${var.app_name}-app-dev-UI"
#   resource_group_name = var.rg_name
#   location            = azurerm_service_plan.tempus_asp_UI.location
#   service_plan_id     = azurerm_service_plan.tempus_asp_UI.id

#   app_settings = {
#     "KeyVaultName"                        = var.kv_name
#   }

#   site_config {
#     application_stack {
#       node_version = "~18"
#     }
#   }

#   identity {
#     type = "SystemAssigned"
#   }

#   tags = {
#     environment = var.environment
#   }
# }


