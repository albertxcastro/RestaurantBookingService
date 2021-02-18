-- Table: Entities.Table

-- DROP TABLE "Entities"."Table";

CREATE TABLE "Entities"."Table"
(
    "Id" bigserial NOT NULL,
    "Capacity" integer NOT NULL,
    "RestaurantId" bigint NOT NULL,
    CONSTRAINT pkey PRIMARY KEY ("Id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("RestaurantId")
        REFERENCES "Entities"."Restaurant" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."Table"
    OWNER to postgres;