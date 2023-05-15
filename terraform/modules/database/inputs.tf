variable "random_integer" {
  type     = string
  nullable = false
}

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

variable "tempus_kv_ap_principal" {
  nullable = false
}
