﻿namespace eReceiptApplication.Contracts;

public class Address
{
    public string CompanyName { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public object Email { get; set; }
}