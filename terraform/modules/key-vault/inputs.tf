variable "environment" {
  type    = string
  default = "dev"
}

variable "location" {
  type     = string
  nullable = false
}

variable "rg_name" {
  type     = string
  nullable = false

}

variable "tenant_id" {
  type     = string
  nullable = false
}

variable "principal_id" {
  type     = string
  nullable = false
}

variable "object_id" {
  type     = string
  nullable = false
}


variable "random_integer" {
  type     = string
  nullable = false
}

variable "db_name" {
  type     = string
  nullable = false
}

variable "sql_server_name" {
  type     = string
  nullable = false
}

variable "admin_login" {
  type     = string
  nullable = false
}

variable "admin_password" {
  type     = string
  nullable = false
}
