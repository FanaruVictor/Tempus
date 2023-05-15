output "db_name" {
  value = azurerm_mssql_database.tempus_db.name
}

output "sql_server_name" {
  value = azurerm_mssql_server.sql_server.name
}

output "admin_login" {
    value = azurerm_mssql_server.sql_server.administrator_login
}

output "admin_password" {
  value = azurerm_mssql_server.sql_server.administrator_login_password
}
