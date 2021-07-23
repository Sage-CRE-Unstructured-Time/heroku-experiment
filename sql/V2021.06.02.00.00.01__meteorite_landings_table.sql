-- Source: https://data.nasa.gov/Space-Science/Meteorite-Landings/gh4g-9sfh
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE meteorite_landings (
    id uuid default uuid_generate_v4() PRIMARY KEY,
    name text null,
    meteorite_id text null,
    nametype text null,
    recclass text null,
    mass numeric null,
    fall text null,
    year timestamp null,
    reclat text null,
    reclong text null,
    geo_location text null
);

COPY meteorite_landings
    (
        name,
        meteorite_id,
        nametype,
        recclass,
        mass,
        fall,
        year,
        reclat,
        reclong,
        geo_location
    )
    FROM '/var/lib/postgresql/custom-datasets/Meteorite_Landings.csv' 
    DELIMITER ',' CSV HEADER;
