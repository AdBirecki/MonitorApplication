# MonitorApplication
1. Aplikacja zbiera dane z api w internecie i zapisuje co minutę aktualne wartości.
2. Dane są pobierane za pomocą przechowywane w tabeli z cenami minerałów.
3. Baza danych stworzona została w oparciu o migracje EF code first i wymaga Ms SQL server. Connection string zdefiniowany appsetings.
4. Controllery Users i Minerals umożliwiają dodawanie użytkowników i odczyt danych. Całość oparta na prostym CQRS z wykorzystaniem Autofac.
5. Testy jednostkowe dodano dla Comand i Handlerów z możliwością zapisu. W testach wykorzystano Nunit i NSubstitute.
