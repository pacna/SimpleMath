## ----------------------------------------------------------------------
## The purpose of this Makefile is to simplify common development tasks.
## ----------------------------------------------------------------------
##

OPT = monogame

.PHONY:run
run: ## Run the app
##
	@if [ "$$OPT" = "terminal" ]; then \
	    dotnet run -terminal; \
	else \
	    dotnet run; \
	fi

.PHONY:publish
publish: ## publish the app as an executable
##
	dotnet publish ./ -c Release -o out

.PHONY:test
test: ## run the tests
##
	dotnet test ./SimpleMath.Tests/

.PHONY:help
help: ## Show this help
##
	@sed -ne '/@sed/!s/##//p' $(MAKEFILE_LIST)