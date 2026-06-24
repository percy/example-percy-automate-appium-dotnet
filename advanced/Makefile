NPM=node_modules/.bin

.PHONY: install clean test test-advanced

$(NPM):
	npm install --no-save @percy/cli@^1.31.13

install: $(NPM)
	dotnet restore

clean:
	rm -rf bin obj node_modules

test test-advanced: install
	$(NPM)/percy app:exec -- dotnet test
