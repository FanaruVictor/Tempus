output "principal_Id" {
  value = azurerm_linux_web_app.tempus_app.identity[0].principal_id
}
