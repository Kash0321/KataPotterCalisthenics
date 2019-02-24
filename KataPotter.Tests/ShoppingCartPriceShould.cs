using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace KataPotter.Tests
{
    public class ShoppingCartPriceShould
    {
        const decimal UNIT_PRICE = 8m;

        [Test]
        public void BeZeroForNoBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(0));
        }

        [Test]
        public void BeUnitPriceForOneBook()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("111"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE));
        }

        [Test]
        public void BeRegularForShoppingCartWithTwoSameBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE * 2));
        }

        [Test]
        public void BeRegularForShoppingCartWithThreeSameBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE * 3));
        }

        [Test]
        public void BeRegularForShoppingCartWithFourSameBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE * 4));
        }

        [Test]
        public void BeRegularForShoppingCartWithFiveSameBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE * 5));
        }

        [Test]
        public void BeDiscountedForShoppingCartWithTwoDifferentBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 2) * 0.95m));
        }

        [Test]
        public void BeDiscountedForShoppingCartWithThreeDifferentBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 3) * 0.90m));
        }

        [Test]
        public void BeDiscountedForShoppingCartWithFourDifferentBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 4) * 0.80m));
        }

        [Test]
        public void BeDiscountedForShoppingCartWithFiveDifferentBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 5) * 0.75m));
        }

        [Test]
        [Ignore("Hay que conseguirlo")]
        public void BeBestDiscountedForShoppingCartWithEdgeCase001()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            // 2 del 1
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));

            // 2 del 2
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));

            // 2 del 3
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));

            //1 del 4
            shoppingCart.AddItem(new Book("444"));

            //1 del 5
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(2 * (UNIT_PRICE * 4 * 0.8m)));
            //resultPrice.Should().Be(new Money((UNIT_PRICE * 5 * 0.75m) + (UNIT_PRICE * 3 * 0.9m)));
        }

        [Test]
        [Ignore("Hay que conseguirlo")]
        public void BeBestDiscountedForShoppingCartWithEdgeCase002()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            // 5 del 1
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));

            // 5 del 2
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));

            // 4 del 3
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));

            // 5 del 4
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));

            // 4 del 5
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(3 * (UNIT_PRICE * 5 * 0.75m) + 2 * (UNIT_PRICE * 4 * 0.8m)));
            //resultPrice.Should().Be(new Money(4 * (UNIT_PRICE * 5 * 0.75m) + 1 * (UNIT_PRICE * 3 * 0.9m)));
        }
    }
}