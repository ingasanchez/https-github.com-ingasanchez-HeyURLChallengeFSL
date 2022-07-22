## Overview

HeyURL! is a service to create awesome friendly URLs to make it easier for people to remember. Our team developed some mock views but they lack our awesome functionality.

## Requirements

1. Implement actions to create a short URL based on a given full URL
2. If URL is not valid, the application returns an error message to the user
3. We want to be able to provide basic click metrics to our users for each URL they generate.
    1. Every time that someone clicks a short URL, it should record that click
    2. the record should include the user platform and browser detected from the user agent
4. We want to create a metrics panel for the user to view the stats for every short URL.
    1. The user should be able to see total clicks per day on the current month
    2. An additional chart with a breakdown of browsers and platforms
5. If someone tries to visit a invalid short URL then it should return a 404 page
1. Business logic and requirements should be fully tested with NUnit
2. Provide EF migrations that can generate a SQL database schema compatible with the models

## Spec for generating short URLs

- It MUST have 5 characters in length e.g. NELNT
- It MUST generate only upper case letters
- It MUST NOT generate special characters
- It MUST NOT generate whitespace
- It MUST be unique
- `ShortUrl` attribute should store only the generated code
