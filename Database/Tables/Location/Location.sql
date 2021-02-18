-- Table: Location.Location

-- DROP TABLE "Location"."Location";

CREATE TABLE "Location"."Location"
(
    "Id" bigint NOT NULL,
    "Longitude" double precision NOT NULL,
    "Latitude" double precision NOT NULL,
    CONSTRAINT "Location_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Location"."Location"
    OWNER to postgres;