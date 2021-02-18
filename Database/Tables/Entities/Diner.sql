-- Table: Entities.Diner

-- DROP TABLE "Entities"."Diner";

CREATE TABLE "Entities"."Diner"
(
    "Id" bigserial NOT NULL,
    "UserID" bigint,
    "RestaurantId" bigint,
    CONSTRAINT "Diner_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("RestaurantId")
        REFERENCES "Entities"."Restaurant" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "UserId" FOREIGN KEY ("UserID")
        REFERENCES "Security"."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."Diner"
    OWNER to postgres;