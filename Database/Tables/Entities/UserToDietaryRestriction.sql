-- Table: Entities.UserToDietaryRestriction

-- DROP TABLE "Entities"."UserToDietaryRestriction";

CREATE TABLE "Entities"."UserToDietaryRestriction"
(
    "UserId" bigint,
    "DietaryRestrictionId" bigint,
    CONSTRAINT "DietaryRestrictionId_fk" FOREIGN KEY ("DietaryRestrictionId")
        REFERENCES "Entities"."DietaryRestriction" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "UserId_fk" FOREIGN KEY ("UserId")
        REFERENCES "Security"."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."UserToDietaryRestriction"
    OWNER to postgres;