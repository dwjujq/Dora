﻿using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Dora.Interception.Test
{
    public class DerivedClassInterceptionFixture
    {

        [Fact]
        public async void InterceptInterface()
        {
            var foobar = new ServiceCollection()
                .AddInterception()
                .AddSingletonInterceptable<IFoobar, Bar>()
                .BuildServiceProvider()
                .GetRequiredService<IFoobar>();
            FakeInterceptorAttribute.Reset<IFoobar>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<IFoobar>());

            foobar = new ServiceCollection()
                .AddInterception()
                .AddSingletonInterceptable<IFoobar, Baz>()
                .BuildServiceProvider()
                .GetRequiredService<IFoobar>();
            FakeInterceptorAttribute.Reset<IFoobar>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<IFoobar>());

            foobar = new ServiceCollection()
                .AddSingleton<IFoobar, Bar>()
                .BuildInterceptableServiceProvider()
                .GetRequiredService<IFoobar>();
            FakeInterceptorAttribute.Reset<IFoobar>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<IFoobar>());

            foobar = new ServiceCollection()
                .AddSingleton<IFoobar, Baz>()
                .BuildInterceptableServiceProvider()
                .GetRequiredService<IFoobar>();
            FakeInterceptorAttribute.Reset<IFoobar>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<IFoobar>());
        }

        [Fact]
        public async void InterceptClass()
        {
            var foobar = new ServiceCollection()
                .AddInterception()
                .AddSingletonInterceptable<Foo, Bar>()
                .BuildServiceProvider()
                .GetRequiredService<Foo>();
            FakeInterceptorAttribute.Reset<Foo>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<Foo>());

            foobar = new ServiceCollection()
                .AddInterception()
                .AddSingletonInterceptable<Foo, Baz>()
                .BuildServiceProvider()
                .GetRequiredService<Foo>();
            FakeInterceptorAttribute.Reset<Baz>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<Baz>());

            foobar = new ServiceCollection()
                .AddSingleton<Foo, Bar>()
                .BuildInterceptableServiceProvider()
                .GetRequiredService<Foo>();
            FakeInterceptorAttribute.Reset<Foo>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<Foo>());

            foobar = new ServiceCollection()
                .AddSingleton<Foo, Baz>()
                .BuildInterceptableServiceProvider()
                .GetRequiredService<Foo>();
            FakeInterceptorAttribute.Reset<Baz>();
            await foobar.Invoke1();
            Assert.Equal("1", FakeInterceptorAttribute.GetResult<Baz>());
        }

        public interface IFoobar
        {
            Task Invoke1();
            Task Invoke2();
        }

        [FakeInterceptor]
        public abstract class Foo : IFoobar
        {
            public virtual Task Invoke1()
            {
                return Task.CompletedTask;
            }

            public abstract Task Invoke2();
        }

        public class Bar : Foo
        {
            public override Task Invoke2()
            {
                return Task.CompletedTask;
            }
        }

        public class Baz : Bar
        {
            public override Task Invoke1()
            {
                return Task.CompletedTask;
            }
        }
    }
}
