-- Table: Entities.DietaryRestriction

-- DROP TABLE "Entities"."DietaryRestriction";

CREATE TABLE "Entities"."DietaryRestriction"
(
    "Id" bigint NOT NULL,
    "Name" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "DietaryRestriction_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."DietaryRestriction"
    OWNER to postgres;