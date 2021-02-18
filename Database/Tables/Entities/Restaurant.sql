-- Table: Entities.Restaurant

-- DROP TABLE "Entities"."Restaurant";

CREATE TABLE "Entities"."Restaurant"
(
    "Id" double precision NOT NULL,
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "LocationId" bigint,
    CONSTRAINT "Id" PRIMARY KEY ("Id"),
    CONSTRAINT "LocationId" FOREIGN KEY ("LocationId")
        REFERENCES "Location"."Location" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."Restaurant"
    OWNER to postgres;