-- Table: Security.Users

-- DROP TABLE "Security"."Users";

CREATE TABLE "Security"."Users"
(
    "Id" bigserial NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE "Security"."Users"
    OWNER to postgres;