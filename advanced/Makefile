NPM=node_modules/.bin
SHELL := /bin/bash
# Percy SDK capture errors and skipped captures are logged and swallowed, so the run is
# failed on them here.
PERCY_ERRORS := Could not take|Could not post screenshot|Error taking screenshot|failed to take screenshot|Percy is not running|percy is disabled|: Error -

.PHONY: install clean test test-advanced

$(NPM):
	npm install --no-save @percy/cli@^1.31.13

install: $(NPM)
	dotnet restore

clean:
	rm -rf bin obj node_modules percy-run.log

test test-advanced: install
	set -o pipefail; $(NPM)/percy exec -- dotnet test 2>&1 | tee percy-run.log
	@! grep -qE '$(PERCY_ERRORS)' percy-run.log || { echo "Percy reported capture errors (see percy-run.log)"; exit 1; }
