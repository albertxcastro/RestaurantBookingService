-- Table: entities.diner

-- DROP TABLE entities.diner

CREATE TABLE entities.diner
(
    "Id" bigserial NOT NULL,
    "UserID" bigint,
    "RestaurantId" bigint,
    CONSTRAINT "Diner_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("RestaurantId")
        REFERENCES entities.restaurant ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "UserId" FOREIGN KEY ("UserID")
        REFERENCES security.users ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.diner
    OWNER to postgres;