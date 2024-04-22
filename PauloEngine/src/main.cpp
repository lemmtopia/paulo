#include "platform.h"

extern bool isRunning;

int main(int argc, char* argv[])
{
	InitializeWindow(640, 480, "Hello world!");

	while (isRunning)
	{
		if (WindowShouldClose())
		{
			isRunning = false;
		}

		Tick();
		Render();
	}

	DestroyWindow();
	return 0;
}