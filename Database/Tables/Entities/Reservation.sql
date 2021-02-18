-- Table: entities.reservation

-- DROP TABLE entities.reservation

CREATE TABLE entities.reservation
(
    "Id" bigserial NOT NULL,
    "DateTime" timestamp with time zone,
    "MarkForDelete" boolean NOT NULL,
    "TableId" bigint NOT NULL,
    "RestaurantId" bigint NOT NULL,
    CONSTRAINT "Reservation_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("RestaurantId")
        REFERENCES entities.restaurant ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT table_fk FOREIGN KEY ("TableId")
        REFERENCES entities.table ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.reservation
    OWNER to postgres;
-- Index: fki_table_fkey

-- DROP INDEX "Entities".fki_table_fkey;

CREATE INDEX fki_table_fkey
    ON entities.reservation USING btree
    ("TableId" ASC NULLS LAST)
    TABLESPACE pg_default;