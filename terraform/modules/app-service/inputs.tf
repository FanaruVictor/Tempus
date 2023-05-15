
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

variable "kv_name" {
  type     = string
  nullable = false
}
