CREATE OR REPLACE FUNCTION entities.fn_get_restaurant_by_criteria
(
    _user_ids bigint[]
)
RETURNS TABLE
(
    id bigint,
    name varchar,
    location_id bigint
)
AS $$
-- =================================================
-- Author: Alberto Castro
-- Description: gets all restaurants that match users´s dietary restrictions
-- =================================================
DECLARE
    number_of_dietary_restrictions integer := 0;
BEGIN
    CREATE TEMP TABLE users_dietary_restrictions
    (
        dietary_restriction_id bigint,
        user_id bigint
    ) ON COMMIT DROP;
    
    INSERT INTO 
        users_dietary_restrictions
    SELECT
        dr.id AS dietary_restriction_id,
        u.id AS user_id
    FROM
        security.users u
        INNER JOIN entities.user_to_dietary_restriction udr
        ON u.id = udr.user_id
        INNER JOIN entities.dietary_restriction dr
        ON dr.id = udr.dietary_restriction_id
    WHERE
        u.id = ANY(_user_ids);

    SELECT COUNT(*) FROM users_dietary_restrictions
    INTO number_of_dietary_restrictions;
    
    raise notice '%', number_of_dietary_restrictions;

    IF number_of_dietary_restrictions > 0 THEN
        RETURN QUERY
        SELECT DISTINCT
            r.id AS id,
            r.name AS name,
            loc.id AS location_id
        FROM
            entities.restaurant r
            INNER JOIN location.location loc
            ON loc.id = r.location_id
            INNER JOIN entities.restaurant_to_dietary_restriction rtdr
            ON r.id = rtdr.restaurant_id
        WHERE 
            rtdr.dietary_restriction_id IN (SELECT dietary_restriction_id FROM users_dietary_restrictions);
    ELSE
        RETURN QUERY
        SELECT DISTINCT
            r.id AS id,
            r.name AS name,
            loc.id AS location_id
        FROM
            entities.restaurant r
            INNER JOIN location.location loc
            ON loc.id = r.location_id;
    END IF;
END;
$$ LANGUAGE PLPGSQL;