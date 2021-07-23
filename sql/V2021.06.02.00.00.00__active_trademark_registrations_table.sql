-- Source: https://data.oregon.gov/Business/Active-Trademark-Registrations/ny3n-dx3v
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE active_trademark_registrations (
    id uuid default uuid_generate_v4() PRIMARY KEY,
    registration_number text null,
    registration_date text null,
    trademark_description text null,
    correspondent_name text null,
    address_1 text null,
    address_2 text null,
    city text null,
    state text null,
    zip text null,
    image_link text null
);

COPY active_trademark_registrations
    (
        registration_number,
        registration_date,
        trademark_description,
        correspondent_name,
        address_1,
        address_2,
        city,
        state,
        zip,
        image_link
    )
    FROM '/var/lib/postgresql/custom-datasets/Active_Trademark_Registrations.csv' 
    DELIMITER ',' CSV HEADER;
