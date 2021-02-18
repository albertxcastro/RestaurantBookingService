-- Table: entities.reservation_to_user

-- DROP TABLE entities.reservationToUser;

CREATE TABLE entities.reservation_to_user
(
    "ReservationId" bigint,
    "UserId" bigint,
    CONSTRAINT "Reservation_pk" FOREIGN KEY ("ReservationId")
        REFERENCES entities.reservation ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "User_pk" FOREIGN KEY ("ReservationId")
        REFERENCES security.users ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE entities.reservation_to_user
    OWNER to postgres;