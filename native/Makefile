CC = gcc
CFLAGS = -shared -fPIC -Wall -Wextra -O2
TARGET = libeditor_syntax.so
SRC = src/editor_syntax.c

all: $(TARGET)
$(TARGET): $(SRC)
	$(CC) $(CFLAGS) -o $(TARGET) $(SRC)

clean:
	rm -f $(TARGET)