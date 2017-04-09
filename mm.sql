CREATE TABLE user_plans(
	plan_id int,
	users_id int,
	CONSTRAINT plan_user_pk PRIMARY KEY (plan_id, users_id),
	CONSTRAINT fk_pu_plan FOREIGN KEY (plan_id) REFERENCES plans (id),
	CONSTRAINT fk_pu_user FOREIGN KEY (users_id) REFERENCES users (id)
);