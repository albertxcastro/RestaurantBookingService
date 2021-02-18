-- Table: entities.dietary_restriction

-- DROP TABLE entities.dietary_restriction

CREATE TABLE entities.dietary_restriction
(
    "Id" bigserial NOT NULL,
    "Name" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "DietaryRestriction_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.dietary_restriction
    OWNER to postgres;