FROM rabbitmq:3-management

# Define environment variables.
ENV RABBITMQ_USER user
ENV RABBITMQ_PASSWORD user
ENV RABBITMQ_PID_FILE /var/lib/rabbitmq/mnesia/rabbitmq

COPY RabbitMq/init.sh /init.sh
RUN chmod +x /init.sh

# Define default command
CMD ["sh", "init.sh"]