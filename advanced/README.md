# Advanced Percy on Automate + Appium-.NET

Exercises the Percy on Automate feature surface via `PercyIO.Appium`'s `PercyOnAutomate` class,
on a mobile-browser session (Chrome on a real Android device). Native apps use App Percy
instead — the CLI's Automate capture needs a browser (JavaScript) context.

## Run locally

```bash
cd advanced
make install
export BROWSERSTACK_USERNAME="<your username>"
export BROWSERSTACK_ACCESS_KEY="<your access key>"
export APPIUM_VERSION="2.19.0"   # optional; BrowserStack appiumVersion
export PERCY_TOKEN="<your project token>"
make test
```

## CI note

`workflow_dispatch`-only.

## Coverage matrix

Source of truth: [`matrix.yml`](./matrix.yml).
