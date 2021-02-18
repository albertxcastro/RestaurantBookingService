-- Table: entities.user_to_dietary_restriction

-- DROP TABLE entities.user_to_dietary_restriction;

CREATE TABLE entities.user_to_dietary_restriction
(
    "UserId" bigint,
    "DietaryRestrictionId" bigint,
    CONSTRAINT "DietaryRestrictionId_fk" FOREIGN KEY ("DietaryRestrictionId")
        REFERENCES entities.dietary_restriction ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "UserId_fk" FOREIGN KEY ("UserId")
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