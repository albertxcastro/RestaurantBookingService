-- Table: entities.diner

-- DROP TABLE entities.diner

CREATE TABLE entities.diner
(
    id bigserial NOT NULL,
    user_id bigint,
    restaurant_id bigint,
    CONSTRAINT "Diner_pkey" PRIMARY KEY ("id"),
    CONSTRAINT "RestaurantId" FOREIGN KEY ("restaurant_id")
        REFERENCES entities.restaurant ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "UserId" FOREIGN KEY ("user_id")
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