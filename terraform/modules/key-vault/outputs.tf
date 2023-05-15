output "tempus_kv" {
  value = azurerm_key_vault.tempus_kv
}

output "kv_policy_pricipal" {
  value = azurerm_key_vault_access_policy.kv_policy_principal
}

output "kv_name" {
  value = azurerm_key_vault.tempus_kv.name
}
