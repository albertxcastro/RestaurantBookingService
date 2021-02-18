-- Table: entities.restaurant

-- DROP TABLE entities.restaurant;

CREATE TABLE entities.restaurant
(
    "Id" bigserial NOT NULL,
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "LocationId" bigint,
    CONSTRAINT "Id" PRIMARY KEY ("Id"),
    CONSTRAINT "LocationId" FOREIGN KEY ("LocationId")
        REFERENCES location.location ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.restaurant
    OWNER to postgres;