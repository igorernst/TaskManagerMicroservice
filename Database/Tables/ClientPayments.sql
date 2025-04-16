-- Table: public.ClientPayments

-- DROP TABLE IF EXISTS public."ClientPayments";

CREATE TABLE IF NOT EXISTS public."ClientPayments"
(
    "Id" bigint NOT NULL,
    "ClientId" bigint NOT NULL,
    "Dt" timestamp without time zone NOT NULL,
    "Amount" money NOT NULL,
    CONSTRAINT "ClientPayments_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."ClientPayments"
    OWNER to postgres;
