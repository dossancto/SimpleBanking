api/watch:
	cd Src/SimpleBanking.API/; dotnet watch run

api/run:
	cd Src/SimpleBanking.API/; dotnet run

test/unit:
	dotnet test Tests/SimpleBanking.Tests.Unit/

test/integration:
	dotnet test Tests/SimpleBanking.Tests.Integration/

test/all: test/unit test/integration 
