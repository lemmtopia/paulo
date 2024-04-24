#include "ECS.h"

#include <stdlib.h>
#include <SDL.h>
#include <SDL_image.h>

extern SDL_Renderer* renderer;

ECS_t InitECS(int n)
{
	ECS_t ecs;
	ecs.len = n;

	ecs.entities = (int*)malloc(n * sizeof(int));
	ecs.transforms = (TransformComponent_t*)malloc(n * sizeof(TransformComponent_t));
	ecs.sprites = (SpriteComponent_t*)malloc(n * sizeof(SpriteComponent_t));
	SDL_Surface* surf = IMG_Load("./assets/happy.png");
	SDL_Texture* tex = SDL_CreateTextureFromSurface(renderer, surf);

	for (int i = 0; i < n; i++)
	{
		ecs.entities[i] = i;
		ecs.transforms[i] = { (double)i * 30, (double)i * 50, 1, 1, 20 };
		ecs.sprites[i] =
		{
			surf->w,
			surf->h,
			tex
		};
	}

	return ecs;
}

void DestroyECS(ECS_t& ecs)
{
	free(ecs.entities);
	free(ecs.transforms);
	free(ecs.sprites);
}

void SystemDraw(ECS_t ecs, SDL_Renderer* renderer)
{
	for (int i = 0; i < ecs.len; i++)
	{
		if (ecs.sprites[i].texture != NULL)
		{
			SDL_Rect rect = {
				(int)ecs.transforms[i].x,
				(int)ecs.transforms[i].y,
				ecs.sprites[i].w * ecs.transforms[i].scaleX,
				ecs.sprites[i].h * ecs.transforms[i].scaleY,
			};
			ecs.transforms[i].rotation++;

			SDL_RenderCopyEx(renderer, ecs.sprites[i].texture, NULL, &rect, ecs.transforms[i].rotation, NULL, SDL_FLIP_NONE);
		}
	}
}