CREATE OR REPLACE FUNCTION entities.fn_get_available_table_list
(
    _restaurant_ids bigint[],
    _required_seats int
)
RETURNS TABLE
(
    id bigint,
    restaurant_id bigint
)
AS $$
-- =================================================
-- Author: Alberto Castro
-- Description: gets all available tables given the restaurand ids, the capacity and if they are not booked.
-- =================================================
BEGIN
    RETURN QUERY
    WITH reservations AS 
    (
        SELECT
            r.table_id
        FROM
            entities.reservation r
        WHERE
			r.mark_for_delete = FALSE
            AND r.date_time IS NOT NULL 
            AND (SELECT EXTRACT(EPOCH FROM current_timestamp - r.date_time)/3600) < 2
    )
    SELECT
        t.id,
        t.restaurant_id
    FROM
        entities.table t
    WHERE
        t.restaurant_id = ANY(_restaurant_ids)
        AND t.capacity >= _required_seats
        AND t.id NOT IN (SELECT table_id FROM reservations);
END;
$$ LANGUAGE PLPGSQL;