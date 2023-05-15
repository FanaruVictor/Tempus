

resource "azurerm_key_vault" "tempus_kv" {
  name                        = "tempus-kv-${var.random_integer}-dev"
  location                    = var.location
  resource_group_name         = var.rg_name
  enabled_for_disk_encryption = true
  tenant_id                   = var.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = false

  sku_name = "standard"

  tags = {
    environment = var.environment
  }
}



resource "azurerm_key_vault_access_policy" "kv_policy_access" {
  key_vault_id = azurerm_key_vault.tempus_kv.id
  tenant_id    = var.tenant_id
  object_id    = var.principal_id

  secret_permissions = [
    "Get", "List"
  ]
}

resource "azurerm_key_vault_access_policy" "kv_policy_principal" {
  key_vault_id = azurerm_key_vault.tempus_kv.id
  tenant_id    = var.tenant_id
  object_id    = var.object_id

  secret_permissions = [
    "Get", "List", "Set", "Delete", "Purge"
  ]
}

