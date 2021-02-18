-- Table: Entities.Reservation

-- DROP TABLE "Entities"."Reservation";

CREATE TABLE "Entities"."Reservation"
(
    "Id" bigint NOT NULL,
    "DateTime" timestamp with time zone,
    "MarkForDelete" boolean NOT NULL,
    "TableId" bigint NOT NULL,
    "RestaurantId" bigint NOT NULL,
    CONSTRAINT "Reservation_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("RestaurantId")
        REFERENCES "Entities"."Restaurant" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT table_fk FOREIGN KEY ("TableId")
        REFERENCES "Entities"."Table" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."Reservation"
    OWNER to postgres;
-- Index: fki_table_fkey

-- DROP INDEX "Entities".fki_table_fkey;

CREATE INDEX fki_table_fkey
    ON "Entities"."Reservation" USING btree
    ("TableId" ASC NULLS LAST)
    TABLESPACE pg_default;