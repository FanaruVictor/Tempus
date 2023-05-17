data "azurerm_client_config" "current" {}

resource "random_integer" "randomizer" {
  min = 1
  max = 50000
}

resource "azurerm_resource_group" "tempus_rg" {
  name     = var.resource-group_name
  location = "West Europe"
}

module "tempus_key_vault" {
  source = "./modules/key-vault"

  environment     = var.environment
  location        = azurerm_resource_group.tempus_rg.location
  rg_name         = azurerm_resource_group.tempus_rg.name
  tenant_id       = data.azurerm_client_config.current.tenant_id
  principal_id    = module.tempus-app.principal_Id
  object_id       = data.azurerm_client_config.current.object_id
  random_integer  = random_integer.randomizer.result
  db_name         = module.tempus_database.db_name
  sql_server_name = module.tempus_database.sql_server_name
  admin_login     = module.tempus_database.admin_login
  admin_password  = module.tempus_database.admin_password
}


module "tempus-app" {
  source = "./modules/app-service"

  environment = var.environment
  location    = azurerm_resource_group.tempus_rg.location
  rg_name     = azurerm_resource_group.tempus_rg.name
  kv_name     = module.tempus_key_vault.kv_name
}


module "tempus_database" {
  source = "./modules/database"

  environment            = var.environment
  location               = azurerm_resource_group.tempus_rg.location
  rg_name                = azurerm_resource_group.tempus_rg.name
  random_integer         = random_integer.randomizer.result
  tempus_kv_ap_principal = module.tempus_key_vault.kv_policy_pricipal
}


resource "azurerm_static_site" "tempus_ss" {
  name                = "tempus-ss"
  resource_group_name = azurerm_resource_group.tempus_rg.name
  location            = azurerm_resource_group.tempus_rg.location
}
