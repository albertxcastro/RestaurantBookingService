-- Table: entities.table

-- DROP TABLE entities.table

CREATE TABLE entities.table
(
    "Id" bigserial NOT NULL,
    "Capacity" integer NOT NULL,
    "RestaurantId" bigint NOT NULL,
    CONSTRAINT pkey PRIMARY KEY ("Id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("RestaurantId")
        REFERENCES entities.restaurant ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.table
    OWNER to postgres;