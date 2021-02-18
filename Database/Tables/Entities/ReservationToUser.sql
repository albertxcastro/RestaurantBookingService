-- Table: Entities.ReservationToUser

-- DROP TABLE "Entities"."ReservationToUser";

CREATE TABLE "Entities"."ReservationToUser"
(
    "ReservationId" bigint,
    "UserId" bigint,
    CONSTRAINT "Reservation_pk" FOREIGN KEY ("ReservationId")
        REFERENCES "Entities"."Reservation" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "User_pk" FOREIGN KEY ("ReservationId")
        REFERENCES "Security"."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Entities"."ReservationToUser"
    OWNER to postgres;