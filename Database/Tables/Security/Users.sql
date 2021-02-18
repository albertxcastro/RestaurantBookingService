-- Table: security.users

-- DROP TABLE security.users

CREATE TABLE security.users
(
    "Id" bigserial NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE security.users
    OWNER to postgres;