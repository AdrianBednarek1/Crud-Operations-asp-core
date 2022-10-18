# L2-Back-end-zadatak
Rješen je prvi dio zadatka.

Zadatak je rješen u Asp.Net Core projektu.

Korišteni su razor pages modeli radi eksperimentiranja i testiranja ispravnosti klasa.

Stoga ne predstavljaju konačno rješenje niti povezanost zadatka. 

Osim VehicleService klase korištene su sljedeće klase:

VehicleRepository - glavni CRUD upravitelj


DB klase:

VehicleMake (na nekim klasama pise Made, zbog zabune)
VehicleModel
VehicleDB


PagingSortingFiltering klase

PaginetedList- definira trenutnu stranicu, broj stranica i sl.
PagingSortingFiltering - glavni upravitelj definiranja stranica
SortingHelp - pomoc oko atributa


Pages klase su uglavnom radi testiranja


Ostale klase po mapama:

Ninject
Automapper
Interfaces- svi interfaci


U Program klasi su definirani pocetni parametri za Page klase:

IVehicleService
IVehicleRepository
