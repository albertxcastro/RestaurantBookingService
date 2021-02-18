-- Table: location.location

-- DROP TABLE location.location;

CREATE TABLE location.location
(
    "Id" bigserial NOT NULL,
    "Longitude" double precision NOT NULL,
    "Latitude" double precision NOT NULL,
    CONSTRAINT "Location_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE location.location
    OWNER to postgres;