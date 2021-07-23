-- Source: https://data.oregon.gov/Revenue-Expense/Salaries-of-State-Agencies-Multi-Year-Report/4cmg-5yp4
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE salaries_of_state_agencies (
    id uuid default uuid_generate_v4() PRIMARY KEY,
    fiscal_year text null,
    agency text null,
    classification text null,
    salary_annual numeric null,
    full_or_part_time text null,
    service_type text null,
    agency_number text null
);

COPY salaries_of_state_agencies
    (
        fiscal_year,
        agency,
        classification,
        salary_annual,
        full_or_part_time,
        service_type,
        agency_number
    )
    FROM '/var/lib/postgresql/custom-datasets/Salaries_of_State_Agencies_-_Multi-Year_Report.csv' 
    DELIMITER ',' CSV HEADER;
