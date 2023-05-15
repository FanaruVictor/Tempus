resource "azurerm_key_vault_secret" "tempus_app_context" {
  name         = "tempus-connection-string"
  value        = "Server=tcp:${var.sql_server_name}.database.windows.net,1433;Initial Catalog=${var.db_name};Persist Security Info=False;User ID=${var.admin_login};Password=${var.admin_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  key_vault_id = azurerm_key_vault.tempus_kv.id

  depends_on = [
    azurerm_key_vault_access_policy.kv_policy_principal
  ]
}
