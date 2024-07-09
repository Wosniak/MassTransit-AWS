namespace PocAwsMasstransit;

public record Company
(
    string Name,
    string Address,
    int BuildingNumber,
    string City,
    string State,
    string PostalCode,
    bool Active
);

