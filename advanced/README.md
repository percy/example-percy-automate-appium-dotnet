# Advanced App Percy on Automate + Appium-.NET

Exercises the App Percy on Automate feature surface via `PercyIO.Appium`'s `PercyOnAutomate` class.

## Run locally

```bash
cd advanced
make install
export BROWSERSTACK_USERNAME="<your username>"
export BROWSERSTACK_ACCESS_KEY="<your access key>"
export APP="bs://<your hashed app id>"
export PERCY_TOKEN="<your project token>"
make test
```

## CI note

`workflow_dispatch`-only.

## Coverage matrix

Source of truth: [`matrix.yml`](./matrix.yml).
