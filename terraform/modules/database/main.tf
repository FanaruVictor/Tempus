resource "random_string" "random_password" {
  length           = 22
  override_special = "@#$%^&*-_+=[]{}|\\:‘,.?/`~“();"
}

resource "random_string" "random_login" {
  length = 12
}



resource "azurerm_mssql_server" "sql_server" {
  name                         = "tempus-sqlserver-${var.random_integer}-dev"
  resource_group_name          = var.rg_name
  location                     = var.location
  version                      = "12.0"
  administrator_login          = random_string.random_login.result
  administrator_login_password = random_string.random_password.result

  tags = {
    environment = var.environment
  }
}


resource "azurerm_mssql_database" "tempus_db" {
  name      = "tempus-database"
  server_id = azurerm_mssql_server.sql_server.id
  sku_name  = "Basic"

  tags = {
    environment = var.environment
  }

  depends_on = [var.tempus_kv_ap_principal]
}

resource "azurerm_mssql_firewall_rule" "firewall_rule" {
  name             = "tempus-sql_firewall-rule"
  server_id        = azurerm_mssql_server.sql_server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}
