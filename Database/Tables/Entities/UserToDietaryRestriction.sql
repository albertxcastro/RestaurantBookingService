-- Table: entities.user_to_dietary_restriction

-- DROP TABLE entities.user_to_dietary_restriction;

CREATE TABLE entities.user_to_dietary_restriction
(
    user_id bigint,
    dietary_restriction_id bigint,
    CONSTRAINT "DietaryRestrictionId_fk" FOREIGN KEY ("dietary_restriction_id")
        REFERENCES entities.dietary_restriction ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "UserId_fk" FOREIGN KEY ("user_id")
        REFERENCES security.users ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.user_to_dietary_restriction
    OWNER to postgres;